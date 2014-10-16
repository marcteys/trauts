//radio
#include <VirtualWire.h> 
byte msg[4];
byte data[4];

// BinaryDataFromProcessing
// These defines must mirror the sending program:
const char HEADER       = 'H';
const char A_TAG    = 'M';
const char B_TAG    = 'X';
const int  TOTAL_BYTES  = 10  ; // the total bytes in a message


void setup()
{
  Serial.begin(9600);
   vw_setup(2400);   
}

void loop(){
  if ( Serial.available() >= TOTAL_BYTES)
  {
     if( Serial.read() == HEADER)
    {
      char tag = Serial.read();
      if(tag == A_TAG)
      {
        //Collect integers
        int a = Serial.read() * 256; 
        a = a + Serial.read();
        int b = Serial.read() * 256;
        b = b + Serial.read();
        int c = Serial.read() * 256;
        c = c + Serial.read();
        int d = Serial.read() * 256;
        d = d + Serial.read();
        
        
        //radio 

        msg[0] = byte(a);
        msg[1] = byte(b);
        msg[2] = byte(c);
        msg[3] = byte(d);


      vw_send((uint8_t *)msg, sizeof(msg)); // On envoie le message 
      
      
       vw_wait_tx(); // On attend la fin de l'envoi

        
        Serial.print("Received integers | a:");
        Serial.print(byte(a));
        Serial.print(", b:");
        Serial.println(byte(b));
        Serial.print(", c:");
        Serial.println(byte(c));
        Serial.print(", d:");
        Serial.println(byte(d));
        
        

      }
      else {
        Serial.print("got message with unknown tag ");
        Serial.write(tag);
      }
    }
  }
}
