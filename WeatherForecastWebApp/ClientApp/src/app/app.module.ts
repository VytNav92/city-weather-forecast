import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { CityWeatherComponent, WeatherPageComponent, WeatherForecastComponent } from './components/index';

@NgModule({
  declarations: [
    CityWeatherComponent,
    WeatherPageComponent,
    WeatherForecastComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [WeatherPageComponent]
})
export class AppModule { }
