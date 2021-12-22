# Clone Aviasales

Clone Aviasales is a clone of the [Aviasales search page](https://www.aviasales.ru/search).

## Requirements
- [.NET 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [TravelPayouts API token](https://support.travelpayouts.com/hc/ru/articles/203956163#chapter_0)

## Installation

```batch
git clone git@github.com:plplmax/clone-aviasales.git
```

```batch
cd clone-aviasales/clone-aviasales
```

```batch
dotnet user-secrets init
```

```batch
dotnet user-secrets set "Token" "<TRAVELPAYOUTS_API_TOKEN>"
```

```batch
dotnet dev-certs https --trust
```

```batch
dotnet watch run
```

Go to https://localhost:5001

## Demo

![demo-1](https://user-images.githubusercontent.com/50287455/147110801-757f3871-f05f-426c-bb36-58c06faf40d3.gif)
#
![demo-2](https://user-images.githubusercontent.com/50287455/147111427-cb6a3b58-4473-4765-be2c-5cd1a48e7f35.gif)

## Contributing
If you want to make small changes, please create a pull request. Major changes will be rejected.

## License
[MIT](https://choosealicense.com/licenses/mit/)
