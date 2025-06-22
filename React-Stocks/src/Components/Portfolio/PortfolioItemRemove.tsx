import { SyntheticEvent } from "react";

interface Props {
  onPortfolioItemRemove: (e: SyntheticEvent) => void;
  item: string;
}

const PortfolioRemoveItem = ({ item, onPortfolioItemRemove }: Props) => {
  return (
    <>
      <form onSubmit={onPortfolioItemRemove}>
        <input readOnly={true} hidden={true} value={item} />
        <button
          type="submit"
          className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500"
        >
          X
        </button>
      </form>
    </>
  );
};

export default PortfolioRemoveItem;
