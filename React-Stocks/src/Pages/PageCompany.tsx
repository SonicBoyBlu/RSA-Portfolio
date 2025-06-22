import { useParams } from "react-router";
import Sidebar from "../Components/CompanyDetail/Controls/Sidebar";
import CompanyDashboard from "../Components/CompanyDetail/CompanyDashboard";

interface Props {}

function PageCompany({}: Props) {
  let { ticker } = useParams();

  return (
    <>
      {ticker ? (
        <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
          <Sidebar />
          <CompanyDashboard ticker={ticker!} children={null}></CompanyDashboard>
        </div>
      ) : (
        <div>Company not found.</div>
      )}
    </>
  );
}

export default PageCompany;
