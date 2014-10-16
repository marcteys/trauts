#include <VirtualWire.h>
//right Voiture
//Motor Init-------------
int PinForward_1 = 6; 
int PinBackward_1 = 11 ; 

int PinForward_2 = 5; 
int PinBackward_2 = 3; 

//Radio Init------

String msg;
boolean ifmsg=false;
const int pttpin=12 ;

////////////////////////////////////

void setup(){
    //Motor
    pinMode(PinForward_1, OUTPUT);
    pinMode(PinBackward_1, OUTPUT);

    pinMode(PinForward_2, OUTPUT);
    pinMode(PinBackward_2, OUTPUT);
    
    //Radio
    Serial.begin(9600);
    Serial.print("Ok ");
    vw_set_rx_pin(9); 
    vw_setup(2400); // initialisation de la librairie VirtualWire à 2000 bauds
    
    vw_rx_start();
}


void loop(){
//Radio 
uint8_t buf[VW_MAX_MESSAGE_LEN]; // Tableau qui va contenir le message reçu (de taille maximum VW_MAX_MESSAGE_LEN)
uint8_t buflen = VW_MAX_MESSAGE_LEN; // Taille maximum de notre tableau

  if (vw_wait_rx_max(200)) // Si un message est reçu dans les 200ms qui viennent
  {
      if (vw_get_message(buf, &buflen)) // On copie le message, qu'il soit corrompu ou non
      {
        Serial.print(buf[0]);
        Serial.print(",");
        Serial.print(buf[1]);
        Serial.print(",");
        Serial.print(buf[2]);
        Serial.print(",");
        Serial.println(buf[3]);
        
        // delay(300);
        analogWrite(PinForward_1,buf[0]);
        analogWrite(PinBackward_1,buf[1]);
        analogWrite(PinForward_2,buf[2]);
        analogWrite(PinBackward_2, buf[3]);
    }
} 

  digitalWrite(13,HIGH);
    
}
