# Backend Revision Plan

## Phase 1 — Critical (Kod Kalitesi)

### 1.1 Magic String → Enum
- [ ] `BuildService.cs` — `CProcessor`, `CMotherboard`, vb. → `ComponentType` enum
- [ ] `BuildService.cs` — `ActionAdded`, `ActionRemoved`, `ActionUpdated` → `BuildAction` enum

### 1.2 Unsafe Type Conversion
- [ ] `BuildController.cs` `GetUserId()` — `int.Parse` → `int.TryParse` ile güvenli hale getir

### 1.3 Exception Leak
- [ ] `BuildService.cs` — `catch (Exception ex) => Result.Error(ex.Message)` → generic mesaj döndür, internal mesajı loglaya

### 1.4 Null/Zero Logic Tutarsızlığı
- [ ] `BuildService.cs:115-123` — `ProcessorId == 0 ? null` mantığı netleştir

## Phase 2 — High (Tutarlılık)

### 2.1 Naming Standardizasyonu
- [ ] `AuthService.cs` — `Kayit`, `Giris` → `Register`, `Login`
- [ ] `ProcessorService.cs` — `Listele`, `Getir`, `Ekle`, `Guncelle`, `Sil` → `List`, `Get`, `Create`, `Update`, `Delete`
- [ ] Diğer tüm servisler aynı şekilde
- [ ] Error mesajları → İngilizce (ya da tamamı Türkçe — birini seç)

### 2.2 EF Core Performans
- [ ] Read-only sorgulara `AsNoTracking()` ekle

## Phase 3 — Medium (Güvenlik)

### 3.1 Logging
- [ ] Tüm servis catch bloklarına `ILogger` ekle

### 3.2 Rate Limiting
- [ ] `Program.cs` — login endpoint'e rate limiting middleware ekle

---
## Review
_Uygulama sonrası buraya eklenecek_
