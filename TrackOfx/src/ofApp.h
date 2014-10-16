#pragma once

#include "ofMain.h"
#include "ofxOpenCv.h"
#include "ofxNetwork.h"


class ofApp : public ofBaseApp{

	public:
		void setup();
		void update();
		void draw();

		void keyPressed(int key);
		void keyReleased(int key);
		void mouseMoved(int x, int y );
		void mouseDragged(int x, int y, int button);
		void mousePressed(int x, int y, int button);
		void mouseReleased(int x, int y, int button);
		void windowResized(int w, int h);
		void dragEvent(ofDragInfo dragInfo);
		void gotMessage(ofMessage msg);
    
    
    
    ofVideoGrabber c[3];
    
    unsigned char *p1 ;
    unsigned char *p2 ;
    unsigned char *p3;
	unsigned char *r ;
    
    unsigned char *blue ;
    unsigned char *red ;
    unsigned char *green ;
    unsigned char *yellow ;
    unsigned char *orange ;
    
    unsigned char *stuart;
    void getTrackStuart();
    void UpdatePStuart(int dec,int _i,unsigned char *_h,unsigned char *_c);
    ofxCvColorImage			cvStuart;
    ofxCvGrayscaleImage 	grayStuart;
    ofPoint lat1,lat2,front,med;
    void drawStuart();
    
    ofVec2f vectorDir;
    
    ofPoint c_Red,c_Green,c_Blue,c_Yellow,c_Orange;
    
    ofPoint cube[5];
    
    int x_decal[3];
    int y_decal[3];
    
    
    void UpdatePColors(unsigned char *_h,int _y,int _i);
    void UpdatePColor(int dec,int _i,int _r, int _g, int _b,unsigned char *_h,unsigned char *_c,int tol);
    
    ofPoint getTrack(unsigned char * _h);
    
    
    int n,n3 ;

    float diffColor(int r1,int g1,int b1,int r2, int g2, int b2);
    ofxCvColorImage			cvColor;
    ofxCvGrayscaleImage 	grayImage;
    ofxCvGrayscaleImage 	grayBg;
    ofxCvGrayscaleImage 	grayDiff;
    int my_abs(int a);
    
    ofxCvContourFinder 	contourCube;
    
    ofxCvContourFinder 	contourStuart;
    
    ofxUDPManager udpConnection;
    
};
