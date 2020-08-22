import { Component, Input } from '@angular/core';
import { City, Weather } from 'src/app/models/index'

@Component({
  selector: 'city-weather',
  templateUrl: './city-weather.component.html'
})

export class CityWeatherComponent {
  @Input() city: City;
  @Input() weather: Weather;
}
