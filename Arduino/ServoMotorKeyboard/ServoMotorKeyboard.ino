#include <Servo.h>

Servo myServo;  // create servo object to control a servo
// twelve servo objects can be created on most boards
 
int pos = 0;    // variable to store the servo position
 
void setup() {
  Serial.begin(9600);
  pinMode(4, INPUT);
  pinMode(5, INPUT);
  myServo.attach(3);  
}
 
void loop() {
 if(digitalRead(4) == HIGH && pos < 180) {
  pos++;
  myServo.write(pos);
  delay(60);
 }

 if(digitalRead(5) == HIGH && pos > 0) {
  pos--;
  myServo.write(pos);
  delay(60);
 }
}
