//osc stuff
import oscP5.*;
import netP5.*;

 byte[]  serialWrite = new byte[4];
int lastMillis=0;


OscP5 oscP5;
NetAddress myRemoteLocation;

String ip = "127.0.0.1";
int port = 57131;


//arduino stuff

import processing.serial.*;
Serial myPort;  // Create object from Serial class
String currentMessage;




void setup() {
  size(400, 400);
  frameRate(25);
  oscP5 = new OscP5(this, port);
  myRemoteLocation = new NetAddress(ip, port);

  //arduino
 // String portName = Serial.list()[0]; //change the 0 to a 1 or 2 etc. to match your port
//  println(portName);
  myPort = new Serial(this, "COM5", 9600);
}


void draw() {
  background(0); 
  
  if(millis() < lastMillis+50) {
   
   lastMillis = millis();
    myPort.write(serialWrite);
  
  }
}

void oscEvent(OscMessage theOscMessage) {

  String messData =  theOscMessage.addrPattern();

  println(messData);
  
  //if(messData.equals(currentMessage)) {
   //  return; 
  //} else {
      //print("### received a new osc message.");
      //println(" addrpattern: "+messData);
      currentMessage = messData;
      
      
      String[] motors = split(messData, '/');
      
     println(messData);
      
    serialWrite[0]=byte(int(motors[0]));
    serialWrite[1]=byte(int(motors[1]));
    serialWrite[2]=byte(int(motors[2]));
    serialWrite[3]=byte(int(motors[3]));
   
    
  //}
    
 


}

