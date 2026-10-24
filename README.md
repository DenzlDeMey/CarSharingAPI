# CarSharingAPI

REST API voor het delen van auto's tussen particulieren: gebruikers vragen een ontlening aan voor een auto, de eigenaar keurt goed of af. Gebouwd als schoolproject voor Odisee (Toegepaste Informatica).

## Stack

- .NET 8, ASP.NET Core Minimal APIs
- CQRS met MediatR, vertical slices per feature
- FluentValidation voor validatie van commands
- JWT-authenticatie met ASP.NET Core Identity
- Entity Framework Core met SQL Server (LocalDB)
- Swagger / OpenAPI (Swashbuckle)
- `Result<T>`-patroon voor foutafhandeling zonder exceptions

## Architectuur

De code is georganiseerd per feature, niet per laag:

```
Features/
  Authentication/   Login, Register
  Borrowings/       Create, Accept, Reject, GetByType
  Cars/             GetAll
```

Elke feature heeft zijn eigen Command of Query, Handler, Endpoint (en waar nodig een Validator). Gedeelde code staat in `Common/` (`Result<T>`, `Error`), `Data/` (`AppDbContext`) en `Entities/`.

## Functionaliteit

- Registreren en inloggen, met een JWT als resultaat
- Alle beschikbare auto's opvragen
- Een ontlening aanvragen (met validatie, bv. je kan je eigen auto niet ontlenen)
- Als eigenaar een ontlening goedkeuren of afwijzen
- Ontleningen opvragen per type
- De auto’s worden via seed-data aangemaakt.

## Opstarten

Vereist: .NET 8 SDK en SQL Server LocalDB (zit bij Visual Studio).

1. Clone de repo.
2. Stel de JWT-sleutel in via user secrets (de repo bevat bewust geen echte sleutel):
   ```
   cd CarSharingAPI
   dotnet user-secrets set "JWT:Secret" "<een-eigen-lange-willekeurige-sleutel>"
   ```
3. Maak de database aan:
   ```
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```
4. Start de API:
   ```
   dotnet run
   ```
5. Open Swagger op `https://localhost:7116/swagger`.

## Demo-accounts

De database wordt gevuld met twee demogebruikers (zie de `SeedData`-migratie):

| Gebruikersnaam | Wachtwoord |
|---|---|
| jan | Test123! |
| piet | Test123! |

Dit zijn wegwerpaccounts voor lokaal testen. De bijhorende hashes staan in de migraties.
