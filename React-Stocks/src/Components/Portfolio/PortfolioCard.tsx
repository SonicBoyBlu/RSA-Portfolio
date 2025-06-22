import { SyntheticEvent } from "react";
import PortfolioRemoveItem from "./PortfolioItemRemove";
import { Link } from "react-router-dom";

interface Props {
  item: string;
  onPortfolioItemRemove: (e: SyntheticEvent) => void;
}

function PortfolioCard({ item, onPortfolioItemRemove }: Props) {
  return (
    <>
      <div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
        <Link
          to={`/company/${item}/company-profile`}
          className="pt-6 text-xl font-bold"
        >
          {item}
        </Link>
        <PortfolioRemoveItem
          onPortfolioItemRemove={onPortfolioItemRemove}
          item={item}
        />
      </div>
    </>
  );
}

export default PortfolioCard;
