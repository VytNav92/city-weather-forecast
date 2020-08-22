import { Component, Input } from '@angular/core';
import { Weather } from 'src/app/models/index'

@Component({
  selector: 'weather-forecast',
  templateUrl: './weather-forecast.component.html'
})

export class WeatherForecastComponent {
  @Input() weather: Weather;
}
