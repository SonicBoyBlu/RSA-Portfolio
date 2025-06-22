import { ChangeEvent, SyntheticEvent, useState } from "react";

import SearchBar from "../Components/Search/SearchBar";
import { CompanySearch } from "../API/company";
import { searchCompanies } from "../API/API-FMP";
import PortfolioList from "../Components/Portfolio/PortfolioList";
import CardList from "../Components/Cards/CardList";

interface Props {}

const PageSearch = ({}: Props) => {
  // State vars
  const [queryCompany, setQueryCompany] = useState("");
  const [searchResults, setSearchResults] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);
  const [portfolioItems, setPortfolioItems] = useState<string[]>([]);

  // END:State vars

  // Local/Session storage - save search history for this session
  /*
  const cacheSearchHistory = "searchHistory";
  const history = sessionStorage.getItem(cacheSearchHistory) || "";
  if (history !== queryCompany) setQueryCompany(history);
  //*/
  // END: storage

  // Event handlers
  const onSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    var v = e.target.value;
    setQueryCompany(v);
    //sessionStorage.setItem(cacheSearchHistory, v);
    console.log(v);
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    console.log(e);
    const result = await searchCompanies(queryCompany);
    if (typeof result === "string") setServerError(result);
    else if (Array.isArray(result.data)) setSearchResults(result.data);
    console.log(result);
  };

  const onPortfolioItemAdd = (e: any) => {
    e.preventDefault();
    const exists = portfolioItems.find((value) => value === e.target[0].value);
    if (exists) return;
    const updatedPortfolio = [...portfolioItems, e.target[0].value];
    setPortfolioItems(updatedPortfolio);
  };
  const onPortfolioItemRemove = (e: any) => {
    e.preventDefault();
    const filtered = portfolioItems.filter((value) => {
      return value !== e.target[0].value;
    });
    setPortfolioItems(filtered);
  };

  // END: Event handlers

  return (
    <>
      <SearchBar
        query={queryCompany}
        onSearchSubmit={onSearchSubmit}
        onSearchChange={onSearchChange}
      />
      {serverError && <h1>{serverError}</h1>}
      <PortfolioList
        portfolioItems={portfolioItems}
        onPortfolioItemRemove={onPortfolioItemRemove}
      />

      <CardList
        searchResults={searchResults}
        onPortfolioItemAdd={onPortfolioItemAdd}
      />
    </>
  );
};

export default PageSearch;
