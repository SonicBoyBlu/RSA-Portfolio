import { v4 as uuid } from "uuid";

type Props = {
  config: any;
  data: any;
};

function Table({ config, data }: Props) {
  // reduce max rows to 20
  const renderedRows = data.slice(0, 20).map((r: any) => {
    return (
      <tr key={uuid()}>
        {config.map((v: any) => {
          return (
            <td
              key={uuid()}
              className="p-4 whitespace-nowrap text-sm font-normal text-gray-900"
            >
              {v.render(r)}
            </td>
          );
        })}
      </tr>
    );
  });
  const renderHeader = config.map((c: any) => {
    return (
      <th
        key={uuid()}
        className="p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
      >
        {c.label}
      </th>
    );
  });
  return (
    <div className="bg-white shadow rounded-lg p-4 sm:p-6 sl:p-8">
      <table>
        <thead className="min-w-full divide-y divided-gray-200 m-5">
          <tr key={uuid()}>{renderHeader}</tr>
        </thead>
        <tbody>{renderedRows}</tbody>
      </table>
    </div>
  );
}

export default Table;
