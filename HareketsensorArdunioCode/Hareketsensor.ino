 #define PIR 2   //Pin tanımlaması(PIR sensörü için)
 bool durum = 0;
 void setup() {
   pinMode(PIR, INPUT);             
   Serial.begin(9600);           
   attachInterrupt(0, hareket, RISING);  //Kesme ayarı
                                         //2. pin "yükselen kenar" olduğunda hareket fonksiyonu çağırılır
 }
 void loop() {
   delay(100);
 }
 void hareket()
 {
   durum = digitalRead(PIR);   //Sensör durumu oku
   Serial.println(durum);      //Sensör durumunu gönder
   delay(100);
 }
