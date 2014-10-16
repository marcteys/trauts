// Processing Sketch

/* SendingBinaryToArduino
* Language: Processing
*/
import processing.serial.*;

byte[] msg = new byte[4];
Serial myPort;  // Create object from Serial class

long lastTime = 0;
void setup()
{  lastTime = millis();
 size(512, 512);
 String portName = Serial.list()[0];
 myPort = new Serial(this, portName, 9600);
}

void draw(){
int o=parseInt(map(mouseX,0,width,0,255));
  msg[0]=0; // moteur droite, vers l'avant
   msg[1]=(byte) o;
    msg[2]=0; //moteur de gauche, vers l'avant
     msg[3]=0; //moteur de gauche , vers l'arriere
  if ( millis() - lastTime > 100 ) {
   myPort.write(msg); 
   lastTime = millis();
 }
}
/*
void serialEvent(Serial p) {
 // handle incoming serial data
 String inString = myPort.readStringUntil('\n');
 if(inString != null) {     
   print( inString );   // echo text string from Arduino
 }
}*/

void mousePressed() {

}
