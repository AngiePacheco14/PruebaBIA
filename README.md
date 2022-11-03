# PruebaBIAAPI
Clonar proyecto y compilar en Visual Studio 2022

Establecer el proyecto PruebaBIAAPI como proyecto de inicio


BD

En la consola del administrador de paquetes correr los siguientes comandos

Add-Migration NewMigration

Update-Database


curl -X 'GET' \
  'https://localhost:7016/api/Energy/CalculateConsumption?Date=2022-10-26&Period=daily'
  
  
