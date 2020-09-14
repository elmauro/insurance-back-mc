# insurance-back-mc

# BackEnd

To working with Insurance Methods

## Asumptions

1. Angular CLI: 9.1.12
2. Node: 12.16.1
3. docker installed
4. Existing Users simulated with a method into MC.Insurance.Back.API "/customers"
5. DataBase in Memory with Microsoft.EntityFrameworkCore.InMemory


## Instalation

a. Install with dotnet

1. ./build.sh
2. dotnet MC.Insurance/bin/Debug/netcoreapp3.1/publish/MC.Insurance.Back.API.dll
3. open http://localhost:5000/weatherforecast or https://localhost:5001/weatherforecast for testing

b. Install with Docker (assuming docker installed on current machine)

1. ./build.sh
2. ./image.sh
2. docker run --name insurance-back -p 5000:80 insurance-back-mc
3. open http://localhost:5000/weatherforecast for testing


## For Unit Testing

  Execute:
  
  1. ./nunit.bat


## API Application Working

You can see the API application working on http://mcinsurancebackapi-env.eba-tvwzp2rf.us-east-1.elasticbeanstalk.com/weatherforecast

## Postman Collection

https://www.getpostman.com/collections/97f17a30683e951f6a41

Insurance

	POST: 	/insurances
	GET: 	/insurances
	GET:	/insurances/{insuranceId}
	PUT:	/insurances/{insuranceId}
	DELETE:	/insurances/{insuranceId}
	
Customer Insurances

	GET:	/customers
	POST:	/customers/{document}/insurances
	GET:	/customers/{document}/insurances
	DELETE:	/customers/{document}/insurances/{insuranceId}

  
## Future Work

1. Code Coverage
2. Security


