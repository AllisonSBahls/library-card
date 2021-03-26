import React from 'react';
import Routes from './Routes';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import './App.css';

function App() {
  return (
    <>
      <Routes/>
      <ToastContainer />
    </>
  );
}

export default App;
