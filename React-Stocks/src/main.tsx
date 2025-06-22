//import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
//import App from "./App.tsx";
import { RouterProvider } from "react-router";
import { router } from "./Routes/Routes.tsx";
import { SkeletonTheme } from "react-loading-skeleton";

createRoot(document.getElementById("root")!).render(
  // StrictMode is for diagnostics, but will cause duplicate async/await calls
  //<StrictMode>
  <SkeletonTheme baseColor="#eaeaea" highlightColor="#ccc">
    <RouterProvider router={router} />
  </SkeletonTheme>
  //</StrictMode>
);
