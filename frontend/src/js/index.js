
import React from 'react';
import { render } from 'react-dom';
import { Provider } from 'react-redux';
import configureStore from './redux/configureStore';
import '../scss/index.scss';
import App from './App';

const store = configureStore();
console.log(store);
render(
    <Provider store={store}>
        <App />
    </Provider>, 
    document.getElementById('content')
);

document.body.classList.remove('loading');
