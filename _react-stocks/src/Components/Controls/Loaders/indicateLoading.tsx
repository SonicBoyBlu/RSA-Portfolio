import { v4 as uuid } from "uuid";
import Skeleton from "react-loading-skeleton";

type Props = {
  type?: string;
  cols?: number;
  rows?: number;
};

const IndicateLoading = ({
  type = "default",
  cols = 1,
  rows = 5,
}: Props): JSX.Element => {
  switch (type) {
    case "ratios":
      return loadingRatioList({ rows });
    case "table":
      return loadingTable({ rows, cols });
    case "tiles":
      return loadingTileHeader({ cols });
    default:
      //return loadingTable();
      //return loadingRatioList();
      return loadingDefault();
  }
};

const loadingDefault = () => {
  const sh = 11;
  return (
    <div className="w-full">
      <Skeleton width={"100%"} />
      <Skeleton width={"95%"} height={sh} />
      <Skeleton width={"90%"} height={sh} />
      <Skeleton width={"95%"} height={sh} />
      <Skeleton width={"90%"} height={sh} />
    </div>
  );
};

const loadingRatioList = ({ rows = 5 }: Props) => {
  const sh = 15;
  const n = rows;
  return (
    <table className="w-full" cellPadding={5}>
      <tr>
        <td width={"90%"}>
          <Skeleton width={"100%"} height={sh} count={n} />
        </td>
        <td width={"10%"}>
          <Skeleton width={"100%"} height={sh} count={n} />
        </td>
      </tr>
    </table>
  );
};

const loadingTable = ({ cols = 5, rows = 5 }: Props) => {
  return (
    <table className="w-full" cellPadding={5}>
      <tbody>
        {Array.from({ length: cols }, (_, _i) => (
          <tr key={uuid()}>
            {Array.from({ length: rows }, (_, _i) => (
              <td key={uuid()}>
                <Skeleton width={"100%"} />
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

const loadingTileHeader = ({ cols = 3 }: Props) => {
  return (
    <table className="w-full" cellPadding={5}>
      <tbody>
        {Array.from({ length: 1 }, (_, _i) => (
          <tr key={uuid()}>
            {Array.from({ length: cols }, (_, _i) => (
              <td key={uuid()}>
                <Skeleton width={"100%"} />
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default IndicateLoading;
