import { v4 as uuid } from "uuid";
type Props = {
  config: any;
  data: any;
};

const RatioList = ({ config, data }: Props) => {
  const renderedCells = config.map((row: any) => {
    return (
      <li key={uuid()} className="py-6 sm:py-6">
        <div className="flex items-center space-x-4">
          <div className="flex-1 min-w-0">
            <div className="text-sm font-medium text-gray-900 truncate">
              {row.label}
            </div>
            <div className="text-sm text-gray-500 truncate">
              {row.subTitle && row.subTitle}
            </div>
          </div>
          <div className="inline-flex items-center text-base font-semibold text-gray-900">
            {row.render(data)}
          </div>
        </div>
      </li>
    );
  });

  return (
    <>
      <div className="bg-white shadow rounded-lg ml-4 mt-4 mb-4 p-4 sm:p-6 w-full">
        <ul className="divide-y divide-gray-200">{renderedCells}</ul>
      </div>
    </>
  );
};

export default RatioList;
