import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { City, Weather } from 'src/app/models/index'

enum FetchStatus {
  NotFetched,
  Fetching,
  Fetched,
  Error,
  NotFound
}

interface CityWeather {
  cityInfo: City;
  currentWeather: Weather;
  hourlyWeather: Weather[];
}

@Component({
  selector: 'weather-page',
  templateUrl: './weather-page.component.html'
})

export class WeatherPageComponent {
  private readonly _httpClient: HttpClient;
  private readonly _baseUrl: string;

  public fetchStatus: FetchStatus = FetchStatus.NotFetched;
  public FetchStatus = FetchStatus;

  public cityWeather: CityWeather;
  public cityInputForm: FormGroup;

  constructor(httpClient: HttpClient, private formBuilder: FormBuilder, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = httpClient;
    this._baseUrl = baseUrl;

    this.cityInputForm = this.formBuilder.group({
      cityName: ['', [Validators.required, Validators.pattern(new RegExp("\\S"))]],
    });
  }

  onInputChanged() {
    this.cityInputForm.value.cityName = this.cityInputForm.value.cityName.trim();
  }

  onSubmit() {
    this.fetchWeather(this.cityInputForm.value.cityName.trim());
  }

  private fetchWeather(cityName: string) {
    this.fetchStatus = FetchStatus.Fetching;
    this.cityInputForm.disable();

    this._httpClient.get<CityWeather>(`${this._baseUrl}weatherforecast/weather?city=${encodeURIComponent(cityName)}`)
      .subscribe(cityWeather => {
        this.cityWeather = cityWeather;
        this.cityWeather.currentWeather.icon = this.createIconUrl(this.cityWeather.currentWeather.icon);
        this.cityWeather.hourlyWeather.forEach(x => x.icon = this.createIconUrl(x.icon));

        this.fetchStatus = FetchStatus.Fetched;
        this.cityInputForm.enable();
      }, error => {
        this.fetchStatus = error.status === 404
          ? FetchStatus.NotFound
          : FetchStatus.Error;

        this.cityInputForm.enable();
      });
  }

  private createIconUrl(iconName: string) {
    return `http://openweathermap.org/img/wn/${iconName}.png`
  }
}
