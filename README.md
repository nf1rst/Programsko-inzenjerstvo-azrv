# Naslov 
Azrv - Aplikacija za razmjenu vještina

## Start aplikacije 

1. Otvaranje u Visual Studio 2026 (!projekt je u dotnet 10 tako da neće radit u Visual Studio 2022!)
2. Posavljanje prog-ing-arzv.Server kao starup project
3. Unutar prog-ing-azrv.Server u folderu Data napravit ponovno folder Database (iako već jedan skriveno postoji)
4. cd u prog-ing-arrv.Server 
'''
5. dotnet ef database update
''' 
6. Pokreni aplikaciju

## Endpointovi dokumentacija 

Endpointovi 

* Authentication endpointovi - /api/auth/*
    - register, logout, login, 

* Controller endpointovi - /api/[controller]/*

1. Skills 
...