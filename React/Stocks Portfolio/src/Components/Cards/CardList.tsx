import { SyntheticEvent } from "react";
import { CompanySearch } from "../../API/company";
import Card from "./Card";
import { v4 as uuid } from "uuid";

interface Props {
  searchResults: CompanySearch[];
  onPortfolioItemAdd: (e: SyntheticEvent) => void;
}

const CardList: React.FC<Props> = ({
  searchResults,
  onPortfolioItemAdd,
}: Props): JSX.Element => {
  return (
    <>
      {searchResults.length > 0 ? (
        searchResults.map((result) => {
          return (
            <Card
              id={result.symbol}
              key={uuid()}
              searchResult={result}
              onPortfolioItemAdd={onPortfolioItemAdd}
            />
          );
        })
      ) : (
        <>
          <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
            No results!
          </p>
        </>
      )}
    </>
  );
};

export default CardList;
