//import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
//import { faSpinner } from "@fortawesome/free-solid-svg-icons";
import { ClipLoader } from "react-spinners";
import "./spinner.css";

type Props = {
  isLoading?: boolean;
  size?: number;
};

const loadingSpinner = ({ isLoading = true, size = 35 }: Props) => {
  return (
    <>
      <div className="loading-spinner" title="Loading...">
        <ClipLoader
          color="#36d7b7"
          loading={isLoading}
          size={size}
          aria-label="Loading Spinner"
          data-testid="loader"
        />
      </div>
    </>
  );
};

export default loadingSpinner;
