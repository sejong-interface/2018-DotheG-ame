#include <Wire.h>
#include <Mouse.h>

#define NUNCHUK_ADDRESS 0x52

int range = 15;
int threshold = range / 4;
int center = range / 2;
 
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

int nunchuk_accelx() {
  return ((uint16_t) nunchuk_data[2] << 2) | ((nunchuk_data[5] >> 2) & 3);
}

int nunchuk_accely() {
  return ((uint16_t) nunchuk_data[3] << 2) | ((nunchuk_data[5] >> 4) & 3);
}

int nunchuk_accelz() {
  return ((uint16_t) nunchuk_data[4] << 2) | ((nunchuk_data[5] >> 6) & 3);
}

float nunchuk_pitch() {
    return atan2((float) nunchuk_accely(), (float) nunchuk_accelz());
}

float nunchuk_roll() {
    return atan2((float) nunchuk_accelx(), (float) nunchuk_accelz());
}

float nunchuk_yaw() {
    return atan2((float) nunchuk_accelx(), (float) nunchuk_accelz());
}

int move_joystick(int thisAxis){
  int reading = thisAxis;

  reading = map(reading, 0, 256, 0, range);

  int distance = reading - center;

  if(abs(distance) < threshold) {
    distance = 0;
  }
  
  return distance;
}


int move_accel(int thisAxis, int type){
  int reading = thisAxis;

  if(type == 0) {
    reading = map(reading, 300, 700, 0, range);
  }

  if(type == 1) {
    reading = map(reading, 250, 600, 0, range);
  }

  int distance = reading - center;

  if(abs(distance) < threshold) {
    distance = 0;
  }
  
  return distance;
}

void setup() {
  Serial.begin(9600);
  Mouse.begin();
  nunchuk_init();
}

void loop() {
  if(nunchuk_read()){
    if (nunchuk_buttonC() == HIGH) {
      Mouse.press(MOUSE_LEFT);
    }
    
    else if(nunchuk_buttonZ() == HIGH) {
      Mouse.press(MOUSE_RIGHT);
    }
    
    else {
      Mouse.release(MOUSE_LEFT);
      Mouse.release(MOUSE_RIGHT);
    }

    Mouse.move(move_joystick(nunchuk_joystickX()), -1 * move_joystick(nunchuk_joystickY()));
    //Mouse.move(move_accel(nunchuk_accelx(),0),move_accel(nunchuk_accely(),1));
  }
  
  /*
  Serial.print("Joystick : ");
  Serial.print(nunchuk_joystickX());
  Serial.print(",");
  Serial.print(nunchuk_joystickY());
  
  Serial.print(" Button : ");
  Serial.print(nunchuk_buttonC());
  Serial.print(",");
  Serial.print(nunchuk_buttonZ());
  */
  
  Serial.print(" Accel : ");
  Serial.print(nunchuk_accelx());
  Serial.print(",");
  Serial.print(nunchuk_accely());
  Serial.print(",");
  Serial.println(nunchuk_accelz());

  /*
  Serial.print(" Roll, Pitch, Yaw : ");
  Serial.print(nunchuk_roll());
  Serial.print(",");
  Serial.print(nunchuk_pitch());
  Serial.print(",");
  Serial.println(nunchuk_yaw());
  */
  
  delay(10);
}
