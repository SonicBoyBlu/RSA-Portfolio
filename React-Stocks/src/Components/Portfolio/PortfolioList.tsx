import { SyntheticEvent } from "react";
import { v4 as uuidv4 } from "uuid";
import PortfolioCard from "./PortfolioCard";

interface Props {
  portfolioItems: string[];
  onPortfolioItemRemove: (e: SyntheticEvent) => void;
}

const PortfolioList = ({ portfolioItems, onPortfolioItemRemove }: Props) => {
  return (
    <>
      <section id="portfolio">
        <h2 className="mb-3 mt-3 text-3xl font-semibold text-center md:text-4xl">
          My Portfolio
        </h2>
        <div className="relative flex flex-col items-center max-w-5xl mx-auto space-y-10 px-10 mb-5 md:px-6 md:space-y-0 md:space-x-7 md:flex-row">
          <>
            {portfolioItems.length > 0 ? (
              portfolioItems.map((i) => {
                return (
                  <PortfolioCard
                    key={uuidv4()}
                    item={i}
                    onPortfolioItemRemove={onPortfolioItemRemove}
                  />
                );
              })
            ) : (
              <h3 className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
                Your portfolio is empty.
              </h3>
            )}
          </>
        </div>
      </section>
    </>
  );
};

export default PortfolioList;
