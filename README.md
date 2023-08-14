First Run these commands

To install EF core 

dotnet tool install --global dotnet-ef

To add migration

dotnet ef migrations add RoleLinkTableFix --startup-project ../Lab.API

To update migration

dotnet ef database update --startup-project ../Lab.API
