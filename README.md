# FastEndpoints Learning Project

This project will be used to learn how to use FastEndpoints.

## Table of Contents

- [License](#license)
- [Author](#author)
- [References](#references)
- [Notes](#notes)

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Author

Created with :heart: by [Kyle Laverty](https://github.com/kylelaverty)

## References

- [FastEndpoints](https://fast-endpoints.com/)
- [Swagger](https://swagger.io/)
- [FastEndpoints Swagger Support](https://fast-endpoints.com/docs/swagger-support)
- [Seq](https://datalust.co/seq)
- [Ardalis Serilog Tutorial](https://www.youtube.com/watch?v=mnPW8PURQOc)

## Notes

- Access API here: http://localhost:5112/api/...
- Access Swagger here: http://localhost:5112/swagger/index.html#/Books/FeaturesBookGetBooksEndpoint
- Start Seq instance: docker run --rm -it -e ACCEPT_EULA=y -p 5341:80 datalust/seq
- Run Docker instance: docker run --rm -it -p 5000:8080 --name testing_something learning-fast-endpoints

### getunleash.io
- docker network create unleash
- docker run --rm -it -e POSTGRES_PASSWORD=some_password -e POSTGRES_USER=unleash_user -e POSTGRES_DB=unleash --network unleash --name postgres postgres
- docker run -p 4242:4242 -e DATABASE_HOST=postgres -e DATABASE_NAME=unleash -e DATABASE_USERNAME=unleash_user -e DATABASE_PASSWORD=some_password -e DATABASE_SSL=false --network unleash --pull=always unleashorg/unleash-server
- Setup API Key in Unleash UI
- set in Authorization section