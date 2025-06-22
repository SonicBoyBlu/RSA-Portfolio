import axios from "axios";
import {
  testDataBalanceSheet,
  testDataCashflowStatement,
  testDataCompany,
  testDataCompanyProfile,
  testDataIncomeStatement,
  testDataKeyMetrics,
  testDataStockComps,
  testDataTenK,
} from "./_testData";

interface GetApiRequest {
  method: string;
  query?: string;
  options?: string;
  dataType?: any;
  returnError?: boolean | false;
}

const useTestData = true;
const timeoutTestData = 1300;
const defaultMaxRows = 10;

export const GetAPI = async ({
  method,
  query,
  options,
  dataType,
  returnError,
}: GetApiRequest): Promise<typeof dataType> => {
  // For offline testing
  if (useTestData === true) return await fetchDemoData({ method });

  console.info("API: LIVE - " + method);
  try {
    const getAPIUrl = (method: string, query?: string, options?: string) => {
      const ApiUrl = "https://financialmodelingprep.com/api/v3/";
      const ApiKey = "apikey=" + process.env.REACT_APP_API_KEY_FMP;

      return `${ApiUrl}${method}/${query ? `${query}/` : ""}?${
        options ? `&${options}&` : ""
      }${ApiKey}`;
    };
    const data = await axios.get<typeof dataType>(
      getAPIUrl(method, query, options)
    );
    //const data = await axios.get(getAPIUrl(method, query, options));
    return data;
  } catch (ex: any) {
    console.error("error message: ", ex.message);

    // Live API error catching
    /*
    if (axios.isAxiosError(ex)) {
      console.log("Error: ", ex.message);
      return ex.message;
    } else {
    console.log("unexpected error: ", ex);
    }
    //*/
    if (returnError === true) return "An unexpected error has occurred.";
  }
};

const fetchDemoData = async ({ method }: GetApiRequest) => {
  console.info("API: TEST - " + method);
  let demoData: any = [];
  switch (method) {
    case "search":
      demoData = testDataCompany;
      break;
    case "profile":
      demoData = testDataCompanyProfile;
      break;
    case "key-metrics-ttm":
      demoData = testDataKeyMetrics;
      break;
    case "income-statement":
      demoData = testDataIncomeStatement;
      break;
    case "balance-sheet-statement":
      demoData = testDataBalanceSheet;
      break;
    case "cash-flow-statement":
      demoData = testDataCashflowStatement;
      break;
    case "stock-peers":
      demoData = testDataStockComps;
      break;
    case "sec_filings":
      demoData = testDataTenK;
      break;
  }
  return new Promise((resolve) => {
    console.log(demoData);
    // Simulate an asynchronous operation
    setTimeout(() => {
      //resolve(demoData);
      resolve({ data: demoData });
    }, timeoutTestData); // Delay for demonstration
  });
};

//#region: FMP API - Supporting methods
export const searchCompanies = async (query: string) => {
  return GetAPI({
    method: "search",
    options: `query=${query}`,
    returnError: true,
  });
};

export const fetchCompanyProfile = async (query: string) => {
  return GetAPI({
    method: "profile",
    query,
  });
};

export const fetchCompanyKeyMetrics = async (query: string) => {
  const data = GetAPI({
    method: "key-metrics-ttm",
    query,
  });
  console.log("KPI fetch");
  console.log(data);
  return data;
};

export const fetchIncomeStatement = async (query: string) => {
  return GetAPI({
    method: "income-statement",
    query,
    options: "period=annual&limit=" + defaultMaxRows,
  });
};

export const fetchBalanceSheet = async (query: string) => {
  return GetAPI({
    method: "balance-sheet-statement",
    query,
    options: "period=annual&limit=" + defaultMaxRows,
  });
};

export const fetchCashflowStatement = async (query: string) => {
  return GetAPI({
    method: "cash-flow-statement",
    query,
    options: "period=annual&limit=" + defaultMaxRows,
  });
};

export const fetchCompanyStockComps = async (_query: string) => {
  // AKA: stock peers
  // Only  available in the paid version of the API so default to ALWAYS return test data
  return testDataStockComps;
  /*
  return await new Promise((resolve) => {
    console.log(testDataStockComps);
    // Simulate an asynchronous operation
    setTimeout(() => {
      resolve({ data: testDataStockComps });
    }, timeoutTestData); // Delay for demonstration
  });
  //*/

  // Pre-wired for paid API license
  /*
  return GetAPI({
    method: "stock_peers",
    options: `symbol=${query}`,
    returnError: true,
  });
  //*/
};

export const fetchTenK = async (query: string) => {
  return GetAPI({
    method: "sec_filings",
    query,
    options: "type=10-k&page=0",
  });
};
//#endregion [END] FMP API - Supporting methods
