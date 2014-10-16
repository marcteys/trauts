//osc stuff
import oscP5.*;
import netP5.*;

OscP5 oscP5;
NetAddress myRemoteLocation;

String ip = "127.0.0.1";
int port = 57131;


//arduino stuff
// Processing Sketch

/* SendingBinaryToArduino
 * Language: Processing
 */
import processing.serial.*;

Serial myPort;  // Create object from Serial class
public static final char HEADER    = 'H';
public static final char A_TAG = 'M';
public static final char B_TAG = 'X';
String currentMessage;

void setup()
{
  size(512, 512);
    frameRate(25);
  oscP5 = new OscP5(this, port);
  myRemoteLocation = new NetAddress(ip, port);

 // String portName = Serial.list()[1];
 // myPort = new Serial(this, portName, 9600);
    myPort = new Serial(this, "COM5", 9600);

}

void draw(){
}

void serialEvent(Serial p) {
  // handle incoming serial data
  String inString = myPort.readStringUntil('\n');
  if(inString != null) {     
    print( inString );   // echo text string from Arduino
  }
}


void oscEvent(OscMessage theOscMessage) {

  String messData =  theOscMessage.addrPattern();
  
  currentMessage = messData;
  
  
  String[] motors = split(messData, '/');
  
 //  println(messData);

     sendMessage(A_TAG,
     int(motors[0]),
     int(motors[1]),
     int(motors[2]),
     int(motors[3]));
    
}
    
void mousePressed() {

}

void sendMessage(char tag, int a, int b, int c, int d){
  // send the given index and value to the serial port
  myPort.write(HEADER);
  myPort.write(tag);
  myPort.write((char)(a / 256)); // msb
  myPort.write(a & 0xff);  //lsb
  myPort.write((char)(b / 256)); // msb
  myPort.write(b & 0xff);  //lsb
  myPort.write((char)(c / 256)); // msb
  myPort.write(c & 0xff);  //lsb
  myPort.write((char)(d / 256)); // msb
  myPort.write(d & 0xff);  //lsb
}
