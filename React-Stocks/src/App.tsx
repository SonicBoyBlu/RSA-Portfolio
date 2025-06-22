import { Outlet } from "react-router";
import Navbar from "./Components/NavBar/Navbar";
import "react-loading-skeleton/dist/skeleton.css";

function App() {
  return (
    <>
      <div className="text-gray-900">
        <Navbar />
        <Outlet />
      </div>
    </>
  );
}

export default App;
