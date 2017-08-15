import { combineEpics } from 'redux-observable';
import { combineReducers } from 'redux';
import 'rxjs';

const identityReducer = (prevState, _) => prevState;

const pingEpic = action$ => 
    action$.filter(action => action.type === 'PING')
           .mapTo({ type: 'PONG' });

export const rootEpic =  pingEpic;//combineEpics(pingEpic);
export const rootReducer = identityReducer;//combineReducers(identityReducer);