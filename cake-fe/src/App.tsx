import { BrowserRouter, Routes, Route } from "react-router-dom";
import { ThemeProvider } from "@/components/theme-provider";
import Page from "@/app/dashboard/page";

import HomePageContainer from "@/pageContainers/HomePageContainer/HomePageContainer";
import LoginPageContainer from "@/pageContainers/LoginPageContainer/LoginPageContainer";
import CategoriesPageContainer from "@/pageContainers/CategoriesPageContainer/CategoriesPageContainer";
import CustomersPageContainer from "@/pageContainers/ClientePageContainer/ClientePageContainer";
import UserPageContainer from "@/pageContainers/UserPageContainer/UserPageContainer";
import SaborPageContainer from "@/pageContainers/SaborPageContainer/SaborPageContainer";
import MarcaPageContainer from "@/pageContainers/MarcaPageContainer/MarcaPageContainer";
import PresentacionPageContainer from "@/pageContainers/PresentacionPageContainer/PresentacionPageContainer";
import DulcePageContainer from "@/pageContainers/DulcePageContainer/DulcePageContainer";
import PrivateRoute from "@/components/PrivateRoute";
import VentaPageContainer from "@/pageContainers/VentaPageContainer/VentaPageContainer";
import RolPageContainer from "./pageContainers/RolPageContainer/RolPageContainer";
import UsuarioPageContainer from "./pageContainers/UsuarioPageContainer/UsuarioPageContainer";

function App() {
  return (
    <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPageContainer />} />
        
            <Route element={<Page />}>
              <Route path="/" element={<HomePageContainer />} />
              <Route path="/categorias" element={<CategoriesPageContainer />} />
              <Route path="/ventas" element={<VentaPageContainer />} />
              <Route path="/clientes" element={<CustomersPageContainer />} />
              <Route path="/usuarioDetalle" element={<UserPageContainer />} />
              <Route path="/sabores" element={<SaborPageContainer />} />
              <Route path="/marcas" element={<MarcaPageContainer />} />
              <Route
                path="/presentaciones"
                element={<PresentacionPageContainer />}
              />
              <Route path="/dulces" element={<DulcePageContainer />} />
              <Route path="/roles" element={<RolPageContainer />} />
              <Route path="/usuarios" element={<UsuarioPageContainer />} />
            </Route>
        
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;
