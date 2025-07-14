import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import PageHome from "../Pages/PageHome";
import PageCompany from "../Pages/PageCompany";
import PageSearch from "../Pages/PageSearch";
import IncomeStatement from "../Components/CompanyDetail/IncomeStatement";
import PageDesignGuide from "../Pages/PageDesignGuide";
import BalanceSheet from "../Components/CompanyDetail/BalanceSheet";
import CashFlow from "../Components/CompanyDetail/CashFlow";
import ProfileOverview from "../Components/CompanyDetail/Overview";
import KeyMetrics from "../Components/CompanyDetail/KeyMetrics";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <PageHome /> },
      { path: "search", element: <PageSearch /> },
      {
        path: "company/:ticker",
        element: <PageCompany />,
        children: [
          { path: "company-profile", element: <ProfileOverview /> },
          { path: "key-metrics", element: <KeyMetrics /> },
          { path: "income-statement", element: <IncomeStatement /> },
          { path: "balance-sheet", element: <BalanceSheet /> },
          { path: "cashflow", element: <CashFlow /> },
        ],
      },
      { path: "design", element: <PageDesignGuide /> },
    ],
  },
]);
