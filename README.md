<a name="readme-top"></a>



<!-- PROJECT SHIELDS -->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![NuGet][nuget-shield]][nuget-url]
[![MIT License][license-shield]][license-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <!-- <a href="https://github.com/adessoTurkey-dotNET/NLocalizator">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a> -->

  <h3 align="center">NLocalizator</h3>

  <p align="center">
    A Run-Time Localization Tool
    <br />
    <a href="https://github.com/adessoTurkey-dotNET/NLocalizator/blob/main/README.md"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/adessoTurkey-dotNET/NLocalizator/tree/main/samples/NLocalizator.Sample">View Demo</a>
    ·
    <a href="https://github.com/adessoTurkey-dotNET/NLocalizator/issues">Report Bug</a>
    ·
    <a href="https://github.com/adessoTurkey-dotNET/NLocalizator/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#usage">Usage</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This tool allows you to build a localization structure whose content can be changed at run-time.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With


[![.Net]][.Net-shield]

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

* Download and install [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### Installation

* Add NLocalizator NuGet package to your project 
```sh
dotnet add package NLocalizator
```

<!-- USAGE EXAMPLES -->
### Usage

##### Create Language Files

Create `*lang-name*.json` file in project directory. This file name is in use configuration of the tool.

For example `tr.json` file:
```json
{
	"Monday" : "Pazartesi",
	"Tuesday" : "Salı",
	"Wednesday" : "Çarşamba",
	"Thursday" : "Perşembe",
	"Friday" : "Cuma",
	"Saturday" : "Cumartesi",
	"Sunday" : "Pazar"
}
```

##### Create The LocalizationBook

```csharp
using NLocalizator;

public class DaysLocalizationBook : ILocalizationBook
{
    public string Monday { get; set; }
    public string Tuesday { get; set; }
    public string Wednesday { get; set; }
    public string Thursday { get; set; }
    public string Friday { get; set; }
    public string Saturday { get; set; }
    public string Sunday { get; set; }
}
```

##### Dependency Injection

```csharp
using NLocalizator;

builder.Services.AddLocalizator<DaysLocalizationBook>(options =>
    options.AddFolderPath(@"*the_folder_path_that_contains_tr.json_file*")
    .AddLanguage("tr"));
```

##### Call the Localizator

```csharp
using NLocalizator;

public class WeatherForecastController : ControllerBase
{
    private readonly Localizator<DaysLocalizationBook> _daysLocalizator;

    public WeatherForecastController(Localizator<DaysLocalizationBook> daysLocalizator)
    {
        _daysLocalizator = daysLocalizator;
    }

    [HttpGet(Name = "GetToday")]
    public string Get()
    {
        var today = _daysLocalizator.GetByName("Monday");

        //or

        today = _daysLocalizator.LocalizationBook.Monday;

        return today;
    }
}
```

##### Change The Language

* Make sure the new `*lang-name*.json` file be in the same folder with `tr.json`

`de.json` file:
```json
{
	"Monday" : "Montag",
	"Tuesday" : "Deinstag",
	"Wednesday" : "Mittwoch",
	"Thursday" : "Donnerstag",
	"Friday" : "Freitag",
	"Saturday" : "Samstag",
	"Sunday" : "Sonntag"
}
```

* Create a new post method in the controller
```csharp
[HttpPost(Name = "SetLanguageToGerman")]
public bool SetLanguageToGerman()
{
    _daysLocalizator.ChangeLanguage("de");
    return true;
}
```

_For more examples, please refer to the [Sample Project](https://github.com/adessoTurkey-dotNET/NLocalizator/tree/main/samples/NLocalizator.Sample)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [x] Add multi language support
- [x] Add multi LocalizationBook support
- [x] Add language change support
- [x] Add examples
- [x] Add license file
- [x] Add NuGet package
- [ ] Add Changelog
- [ ] Add structral comments for methods
- [ ] Add tests

See the [open issues](https://github.com/othneildrew/Best-README-Template/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.md` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

[![LinkedIn][linkedin-shield]][linkedin-url]

Tolga Çakır - tolgacakirx@gmail.com

Project Link: [https://github.com/adessoTurkey-dotNET/NLocalizator](https://github.com/adessoTurkey-dotNET/NLocalizator)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/adessoTurkey-dotNET/NLocalizator.svg?style=for-the-badge
[contributors-url]: https://github.com/adessoTurkey-dotNET/NLocalizator/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/adessoTurkey-dotNET/NLocalizator.svg?style=for-the-badge
[forks-url]: https://github.com/adessoTurkey-dotNET/NLocalizator/network/members
[stars-shield]: https://img.shields.io/github/stars/adessoTurkey-dotNET/NLocalizator.svg?style=for-the-badge
[stars-url]: https://github.com/adessoTurkey-dotNET/NLocalizator/stargazers
[issues-shield]: https://img.shields.io/github/issues/adessoTurkey-dotNET/NLocalizator.svg?style=for-the-badge
[issues-url]: https://github.com/adessoTurkey-dotNET/NLocalizator/issues
[license-shield]: https://img.shields.io/github/license/adessoTurkey-dotNET/NLocalizator.svg?style=for-the-badge
[license-url]: https://github.com/adessoTurkey-dotNET/NLocalizator/blob/main/LICENSE.md
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/tolgacakirx
[product-screenshot]: images/screenshot.png
[.Net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[.Net-shield]: https://img.shields.io/badge/.NET-5C2D91?
[nuget-shield]: https://img.shields.io/nuget/v/NLocalizator?style=for-the-badge
[nuget-url]: https://www.nuget.org/packages/NLocalizator