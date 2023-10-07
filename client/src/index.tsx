import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import 'semantic-ui-css/semantic.min.css'
import reportWebVitals from './reportWebVitals';
import { StoreProvider } from './stores/core/StoreProvider';
import FlashCardStore from './stores/features/flashCardStore';
import { RouterProvider } from 'react-router-dom';
import { router } from './routes/Routes';
import { Store } from './stores/core/store';
import ModalStore from './stores/common/modalStore';
import AuthStore from './stores/features/authStore';

const store: Store = {
  modalStore: new ModalStore(),
  authStore: new AuthStore(),
  flashCardStore: new FlashCardStore()
}
const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
      <StoreProvider store={store}>
        <RouterProvider router={router} />
      </StoreProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
