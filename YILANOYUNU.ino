// --- PIN TANIMLAMALARI ---
/*const int pinX = A0;    // Joystick X Ekseni
const int pinY = A1;    // Joystick Y Ekseni
const int pinSW = 2;    // Joystick Buton (SW) pini

// --- DEĞİŞKENLER ---
bool oyunBasladi = false; // Oyunun durumunu takip eder

void setup() {
  // Seri haberleşmeyi başlat (C# tarafıyla aynı hızda: 9600)
  Serial.begin(9600); 
  
  // Joystick butonunu giriş olarak ayarla (Dahili dirençle)
  pinMode(pinSW, INPUT_PULLUP); 
}

void loop() {
  // Analog ve dijital değerleri oku
  int xVal = analogRead(pinX);
  int yVal = analogRead(pinY);
  int swVal = digitalRead(pinSW);

  // --- 1. Y EKSENİ (YUKARI - AŞAĞI) VE MOD SEÇİMİ ---
  if (yVal < 200) {
    // Joystick Yukarı
    Serial.println("YUKARI");
    delay(150);
  } 
  else if (yVal > 800) {
    // Joystick Aşağı çekildiğinde iki ihtimal var:
    if (!oyunBasladi) {
      // OYUN BAŞLAMADIYSA: C#'taki CheckBox'ı değiştir
      Serial.println("DUVAR_MOD_DEGISTIR");
      delay(400); // Modun çok hızlı değişmemesi için bekleme süresi
    } 
    else {
      // OYUN BAŞLADIYSA: Yılanı aşağı hareket ettir
      Serial.println("ASAGI");
      delay(150);
    }
  }

  // --- 2. X EKSENİ (SOL - SAĞ) ---
  if (xVal < 200) {
    // Joystick Sol
    Serial.println("SOL");
    delay(150);
  } 
  else if (xVal > 800) {
    // Joystick Sağ
    Serial.println("SAG");
    delay(150);
  }

  // --- 3. BAŞLATMA BUTONU (D2 / SW) ---
  if (swVal == LOW) {
    // Butona basıldığında oyunu başlat komutu gönder
    Serial.println("START");
    oyunBasladi = true; // Artık mod seçimi değil, yön kontrolü yapılacak
    delay(500); // Titreşimi önlemek için uzun bekleme
  }

  // C# tarafı "Oyun Bitti" mesajı gönderirse (isteğe bağlı geliştirme)
  if (Serial.available() > 0) {
    String mesaj = Serial.readStringUntil('\n');
    if (mesaj.indexOf("FINISH") >= 0) {
      oyunBasladi = false; // Tekrar mod seçimi yapılabilir hale gelir
    }
  }
}*/