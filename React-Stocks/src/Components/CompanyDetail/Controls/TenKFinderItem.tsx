import { Link } from "react-router-dom";
import { CompanyTenK } from "../../../API/company";

type Props = {
  metrics: CompanyTenK;
};

function TenKFinderItem({ metrics }: Props) {
  const dateFiled = new Date(metrics.fillingDate).getFullYear();
  return (
    <Link
      reloadDocument
      to={metrics.finalLink}
      type="button"
      className="inline-flex items-center p-4 text-md text-white bg-lightGreen rounded-md"
    >
      10k: {metrics.symbol} ({dateFiled})
    </Link>
  );
}

export default TenKFinderItem;
