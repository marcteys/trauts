#include "ofApp.h"

//--------------------------------------------------------------
void ofApp::setup(){

    //UDP
     udpConnection.Create();
	udpConnection.Connect("192.168.0.13",11999);
	udpConnection.SetNonBlocking(true);

    ///// SETUP VARS

    x_decal[0]=0;
    y_decal[0]=0;

    x_decal[1]=7;
    y_decal[1]=-54;


    x_decal[2]=12;
    y_decal[2]=-94;


    ////

    vector<ofVideoDevice> devices = c[0].listDevices();

    for(unsigned  int i = 0; i < devices.size(); i++){
		cout << devices[i].id << ": " << devices[i].deviceName;
        if( devices[i].bAvailable ){
            cout << endl;
        }else{
            cout << " - unavailable " << endl;
        }
	}

    grayImage.allocate(320,240*3);

    red= new unsigned char[320*240*3];
    blue= new unsigned char[320*240*3];
    green = new unsigned char[320*240*3];
    yellow = new unsigned char[320*240*3];
    orange = new unsigned char[320*240*3];

    stuart= new unsigned char[320*(240*3)*3];

    c[0].setDeviceID(0);

    c[0].initGrabber(320,240);
    c[1].setDeviceID(2);
    c[1].initGrabber(320,240);
    c[2].setDeviceID(1);
    c[2].initGrabber(320,240);

    p1 = c[0].getPixels();
    p2 = c[1].getPixels();
    p3 = c[2].getPixels();

    int w = 320;
    int h = 240;
    n = (w*h*3);
    n3=n*3;


}

//--------------------------------------------------------------
void ofApp::update(){


    for(unsigned  int i=0;i<3;i++){
        c[i].update();
    }



    for (unsigned  int i=0; i<n3; i+=3) {
        int y=i/n;

        if(y==0 ){
           UpdatePColors(p1,y,i);
             UpdatePStuart(y*n,i,stuart,p1);

          }else if(y==1){
            UpdatePColors(p2,y,i);
              UpdatePStuart(y*n,i,stuart,p2);


          }else{
             UpdatePColors(p3,y,i);
              UpdatePStuart(y*n,i,stuart,p3);
          }

        }


    cube[0]=getTrack(red);
    cube[1]=getTrack(green);
    cube[2]=getTrack(blue);
    cube[3]=getTrack(yellow);
    cube[4]=getTrack(orange);
    getTrackStuart();

 string message="{\"cubes\": {";

    for(unsigned int i=0; i< 5 ; i++){


    string msg_cube="\"cube_"+ofToString(i)+"\":{";

    if(cube[i].x>0 && cube[i].y>0){

        msg_cube+="\"visible\" : true,";

    }else{

        msg_cube+="\"visible\" : false,";

    }


        msg_cube+="\"x\":"+ofToString(cube[i].x)+",";
         msg_cube+="\"y\":"+ofToString(cube[i].y);


        if(i!=4){
       msg_cube+="},";


        }else{
        msg_cube+="}";

        }


        message+=msg_cube;
        }
        message+="}}";





	udpConnection.Send(message.c_str(),message.length());




}


//--------------------------------------------------------------
void ofApp::draw(){


    c[0].draw(300,0);
     c[1].draw(300,240);
     c[2].draw(300,240*2);

    //cout << ofGetFrameRate() << endl;
   /* ofImage u;
    u.setFromPixels(blue, 320, 240*3, OF_IMAGE_GRAYSCALE);
    u.draw(0,0);

     ofImage u2;
    u2.setFromPixels(red, 320, 240*3, OF_IMAGE_GRAYSCALE);
    u2.draw(320,0);*/


      ofSetColor(240);
    ofRect(0,0,320,(240*3)+y_decal[1]+y_decal[2]);

    ofSetColor(0);
    for (int i=0; i<5; i++) {
        ofCircle(cube[i].x,cube[i].y,10,10);
     }
   ofSetColor(255);
    drawStuart();

}


//--------------------------------------------------------------
//--------------------------------------------------------------


ofPoint ofApp::getTrack(unsigned char *_h){

    int x,y;
    grayImage.setFromPixels(_h,320,240*3);
   // grayImage.blurHeavily();
    grayImage.blur(5);
    grayImage.threshold(4);
    contourCube.findContours(grayImage, 5, 320*240/3, 1, true);


   grayImage.draw(0,0);

    if(contourCube.nBlobs){
    x= contourCube.blobs[0].centroid.x;
    y= contourCube.blobs[0].centroid.y;

    int decal_id=y/240;
    x=x+x_decal[decal_id];
    y=y+y_decal[decal_id];



    }else{
        x=-1;
        y=-1;
    }

   return  ofPoint(x,y);
}
//--------------------------------------------------------------

void ofApp::getTrackStuart(){



    cvStuart.setFromPixels(stuart, 320, 240*3);
    grayStuart=cvStuart;

    grayStuart.threshold(252);
   // grayStuart.blur(5);
    // grayStuart.threshold(252);
    grayStuart.draw(0,0);



    contourStuart.findContours(grayStuart, 3, 120, 6, true);

    vector<ofPoint> dev1,dev2,dev3;
    vector<ofPoint> stuartBlobs;


    for(unsigned int i=0; i<contourStuart.nBlobs;i++){

        int id=contourStuart.blobs[i].centroid.y/240;

        if(id==0){

            dev1.push_back(contourStuart.blobs[i].centroid);


        }else if(id==1){

           dev2.push_back(contourStuart.blobs[i].centroid);

        }else{

             dev3.push_back(contourStuart.blobs[i].centroid);
        }

        }




    if(dev1.size()==3){
        stuartBlobs=dev1;

    }else if(dev2.size()==3){
        stuartBlobs=dev2;

    }else if(dev3.size()==3){
        stuartBlobs=dev3;

    }





        float minDist=999999999;
        int i1=0;
        int i2=0;
        int i3=0;
        int nbBStuart=stuartBlobs.size();

       if(nbBStuart) int decal_id=stuartBlobs[0].y/240;


        for(int i=0;i<nbBStuart;i++){
            for(int j=0;j<nbBStuart;j++){

                ofPoint p1= stuartBlobs[i];
                ofPoint p2= stuartBlobs[j];
                float tempoDist=ofDist(p1.x, p1.y,p2.x, p2.y);

                if(tempoDist<minDist && i!=j){
                    minDist=tempoDist;
                    i2=i;
                    i1=j;
                }

            }


        for(int i=0;i<3;i++){ if(i2!=i && i1!=i){ i3=i; } }

        lat1=stuartBlobs[i1];
        lat2=stuartBlobs[i2];
        front=stuartBlobs[i3];
        med= ofPoint((lat1.x+lat2.x)/2,(lat1.y+lat2.y)/2);
        vectorDir= ofVec2f(front.x-med.x, front.y-med.y);

    }






   //contourStuart.draw(0,0);

}
//--------------------------------------------------------------

void ofApp::drawStuart(){

    ofSetColor(255,0,0);
    ofCircle(lat1.x, lat1.y,3);
    ofCircle(lat2.x, lat2.y,3);

    ofSetColor(0,0,255);
    ofCircle(front.x, front.y,3);
    ofCircle(med.x, med.y,3);
    ofLine(med.x, med.y, front.x, front.y);
    ofSetColor(255);

}

//--------------------------------------------------------------


void ofApp::UpdatePColors(unsigned char *_h,int _y,int _i){



    UpdatePColor(_y*n,_i,255, 0,0, _h,red,85);
    UpdatePColor(_y*n,_i,0, 255,0, _h,green,73);
    UpdatePColor(_y*n,_i,0, 0,255, _h,blue,77);
    UpdatePColor(_y*n,_i,170, 244, 41, _h,yellow,85);
    UpdatePColor(_y*n,_i,255,112,0, _h,orange,85);



}

//--------------------------------------------------------------
void ofApp::UpdatePColor(int dec,int _i,int _r, int _g, int _b,unsigned char *_h,unsigned char *_c,int tol){

    if(diffColor(_r,_g,_b,_h[_i-dec],_h[_i+1-dec],_h[_i+2-dec])>tol){
        _c[_i/3]=255;
    }else{
        _c[_i/3]=0;
    }

}

//--------------------------------------------------------------
void ofApp::UpdatePStuart(int dec,int _i,unsigned char *_h,unsigned char *_c){

    stuart[_i]=_c[_i-dec];
    stuart[_i+1]=_c[_i+1-dec];
    stuart[_i+2]=_c[_i+2-dec];

}

//--------------------------------------------------------------
float ofApp::diffColor(int r1,int g1,int b1,int r2, int g2, int b2){

    int diffR  = abs(r1 - r2);
    int diffG  = abs(g1 - g2);
    int diffB  = abs(b1 - b2);

    float pctDiffRed   = (float)diffR   / 255;
    float pctDiffGreen = (float)diffG / 255;
    float pctDiffBlue   = (float)diffB  / 255;

    return 100-((pctDiffRed + pctDiffGreen + pctDiffBlue) / 3 * 100);


}


//--------------------------------------------------------------
int ofApp::my_abs(int a) {
    int mask = (a >> (sizeof(int) * CHAR_BIT - 1));
    return (a + mask) ^ mask;
}






//--------------------------------------------------------------
void ofApp::keyPressed(int key){
c[0].videoSettings();
}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){

}
