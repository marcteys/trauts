///radio
#include <VirtualWire.h> 
byte msg[4];
byte data[4];


void setup() // Fonction setup()
{
 //unity
 Serial.begin(9600); 
 //radio
  vw_setup(2400);   
    
}

void loop() // Fonction loop()
{

//radio 

 
 
 while (Serial.available()<4) {} // Wait 'till there are 9 Bytes waiting
for(int n=0; n<4; n++){
  msg[n] = Serial.read();
  
}
  
 vw_send((uint8_t *)msg, sizeof(msg)); // On envoie le message 


   vw_wait_tx(); // On attend la fin de l'envoi
   Serial.println("Done !"); // On signal la fin de l'envoi
   delay(10); // Et on attend 1s pour pas flooder
   
   
}
