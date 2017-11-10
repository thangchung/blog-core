import { combineEpics } from 'redux-observable';
import { reducer as formReducer } from 'redux-form';
import * as WeatherForecasts from './WeatherForecasts';
import * as Counter from './Counter';
import * as Blog from './blog';
import * as Common from './common';

// The top-level state object
export interface ApplicationState {
  counter: Counter.CounterState;
  weatherForecasts: WeatherForecasts.WeatherForecastsState;
  blog: Blog.BlogState;
  common: Common.CommonState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
  form: formReducer,
  counter: Counter.reducer,
  weatherForecasts: WeatherForecasts.reducer,
  blog: Blog.reducer,
  common: Common.reducer
};

export const rootEpic = combineEpics(...Blog.blogEpics);

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
  (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
