# EducationProject

Proje EducationGroup ve Education adında 2 service ve 1 adet gateway'den oluşmaktadır. Api gateway olarak Ocelot kullanılmıştır.
Her bir service cqrs pattern'i kullanılarak onion architecture'a göre hazırlanmıştır. 
(Api, Application, Domain ve Persistence projelerinden oluşmaktadır.)

Unit testler xunit, moq ve bogus kütüphaneleri kullanılarak yazılmıştır.

Db olarak PostgreSql kullanılmıştır. Her service kendisine ait olan veritabanlarını kullanır. Bunlar educationdb ve educationgroupdb.
Bu veritabanlarındaki kayıtlar her 30 dakikda bir redis cache'de tutulmaktadır.

Projenin ana dizininde örnek requestlerin bulunduğu postman collection ve docker-compose dosyaları bulunmaktadır.
Ana dizinde "docker-compose up" komutu ile veritabanı ve servislere docker üzerinden erişilebilir. 

http://localhost:5119/services/educationgroups/educationgroup

http://localhost:5119/services/educations/education
