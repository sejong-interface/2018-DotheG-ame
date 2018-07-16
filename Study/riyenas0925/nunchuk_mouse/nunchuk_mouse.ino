#include <Wire.h>
#include <Mouse.h>

#define NUNCHUK_ADDRESS 0x52

int nunchuk_data[6];

int nunchuk_read(){
  int i;
    
  Wire.requestFrom(NUNCHUK_ADDRESS, 6);

  for (i = 0; i < 6 && Wire.available(); i++){
    nunchuk_data[i] = Wire.read();
  }
      
  Wire.beginTransmission(NUNCHUK_ADDRESS);
  Wire.write(0x00);
  Wire.endTransmission();
  
  return i == 6;
}

void nunchuk_init(){
  Wire.begin();
  Wire.setClock(400000);
   
  Wire.beginTransmission(NUNCHUK_ADDRESS);
  Wire.write(0xF0);
  Wire.write(0x55);
  Wire.endTransmission();
}

int nunchuk_buttonZ() {
  return (~nunchuk_data[5] >> 0) & 1;
}

int  nunchuk_buttonC() {
  return (~nunchuk_data[5] >> 1) & 1;
}

int nunchuk_joystickX() {
  return nunchuk_data[0];
}

int nunchuk_joystickY() {
  return nunchuk_data[1];
}

void setup() {
  Serial.begin(9600);
  Mouse.begin();
  nunchuk_init();
}

void loop() {
  if(nunchuk_read()){
    if (nunchuk_buttonZ() == HIGH) {
      Mouse.press(MOUSE_LEFT);
    }
    
    else if(nunchuk_buttonC() == HIGH) {
      Mouse.press(MOUSE_RIGHT);
    }
    
    else {
      Mouse.release(MOUSE_LEFT);
      Mouse.release(MOUSE_RIGHT);
    }
  }
  delay(10);
}
