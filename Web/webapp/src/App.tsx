import { CssBaseline, ThemeProvider } from '@mui/material';
import { createTheme } from '@mui/material/styles';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { routes as appRoutes } from './routes';
import AuthStore from './stores/AuthStore';
import Layout from './components/Layout/./Layout'
import { IAppStore } from './interfaces/appStore';
import { createContext, useState } from 'react';

const store: IAppStore = {
  'authStore': new AuthStore()
};
export const AppStoreContext = createContext(store);

function App() {
  const theme = createTheme({
    palette: {
      primary: {
        light: '#63b8ff',
        main: '#0989e3',
        dark: '#005db0',
        contrastText: '#000'
      },
      secondary: {
        main: '#4db6ac',
        light: '#82e9de',
        dark: '#00867d',
        contrastText: '#000'
      },
    }
  });

  const [appStore, setAppStore] = useState(store)

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <AppStoreContext.Provider value={appStore}>
          <Layout>
            <Routes>
              {appRoutes.map((route) => (
                <Route
                  key={route.key}
                  path={route.path}
                  element={<route.component />}
                />
              ))}
            </Routes>
          </Layout>
        </AppStoreContext.Provider>
      </Router>
    </ThemeProvider>
  );
};

export default App;
