void setup() {
  pinMode(11, OUTPUT);
  pinMode(9, OUTPUT);
}

void loop() {
  digitalWrite(11, HIGH); // Green LED ON
  digitalWrite(9, LOW);   // Red LED OFF
  delay(1000);            // 1 sec delay

  digitalWrite(11, LOW);  // Green LED OFF
  digitalWrite(9, HIGH);  // Red LED ON
  delay(1000);            // 1 sec delay
}
