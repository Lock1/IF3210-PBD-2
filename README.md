# Tugas Besar 2 - IF3210 - Platform Based Development
## Deskripsi
Tugas besar kedua IF3210 adalah **Survival Shooter: Extended** yang dibuat diatas game engine Unity.
Tugas ini melanjutkan dari latihan **Survival Shooter** yang telah diberikan dikelas.


## Cara kerja
Program dibuat dan dites pada `Unity Editor 2020.3.32f1 LTS`.
Program telah memenuhi semua spesifikasi yang diberikan oleh asisten.

1. Bagian atribut player ditambahkan pada HUDCanvas. 
2. Orb menggunakan sphere sebagai model dan menambakan interaksi menggunakan sphere collider.
3. Additional mobs dipenuhi dengan menambah 3 mob sesuai dengan permintaan spesifikasi.
4. Terdapat 2 game mode yaitu Zen dan Wave, Zen tidak memiliki batas dan Wave memiliki batas akhir 15 wave.
Wave memiliki pola enemy pool secara periodik 3 wave.
Setiap wave berkontribusi tambahan 3 weight.
5. Kedua weapon upgrade ditambahkan beserta UI upgradenya.
6. Local scoreboard menggunakan `Sqlite` dan `PlayerPrefs` untuk menyimpan scoreboard dan nickname.
7. Main menu telah memenuhi nickname, game mode selection, dan scoreboard.
8. Game over mengikuti layar yang diajarkan pada tutorial.

Selain spesifikasi wajib terdapat weapon upgrade explosion untuk memenuhi bonus.


## Library yang digunakan
Library yang digunakan adalah `Sqlite` untuk menyimpan data 
scoreboard. Selain itu program menggunakan library bawaan Unity dan C#.


## Screenshot aplikasi
![Zen](other/img/zen.jpg)
![Normal](other/img/survival.jpg)
![Wave](other/img/wave.jpg)
![Explode](other/img/explode.jpg)


## Pembagian Kerja
NIM      | Tugas
-------- | -----
13519140 | Gameover, Wave, Orb
13519146 | Main Menu, Scoreboard, HUD
13519214 | Upgrade, Zen, Mobs