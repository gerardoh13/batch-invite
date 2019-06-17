import React from "react";
import PropTypes from "prop-types";
import logger from "../logger";
import * as eventContributorsService from "../services/eventContributorsService";
import { toast } from "react-toastify";

const _logger = logger.extend("confirmation");

class confirmEventInvite extends React.Component {
  state = {
    seconds: 3,
    success: null
  };

  componentDidMount() {
    let token = this.getToken();
    eventContributorsService
      .confirm(token)
      .then(this.successConfirm)
      .catch(this.onError);
  }
  getToken = () => {
    let token = this.props.location.search.split(":")[1];
    _logger(token);
    return token;
  };

  successConfirm = () => {
    this.countDown();
    this.setState(
      () => {
        return {
          success: true
        };
      },
      () => setTimeout(() => this.props.history.push("/home"), 3000)
    );
  };
  onError = error => {
    this.setState(() => {
      return {
        success: false
      };
    });
    let errorMessage = error.toString();
    toast.error(errorMessage);
  };

  countDown = () => {
    this.interval = setInterval(() => {
      this.state.seconds > 0
        ? this.setState(prevState => {
            return {
              seconds: prevState.seconds - 1
            };
          })
        : this.stop();
    }, 1000);
  };

  stop = () => {
    clearInterval(this.interval);
  };

  render() {
    return (
      <div className="container">
        <div className="abs-center">
          {this.state.success === true && (
            <div className="text-center">
              <img
                style={{ width: "15%" }}
                src="https://www.kizoa.fr/img/e8nZC.gif"
                alt=""
              />
              <h2 className="mt-2 text-bold">
                Thank you for accepting our invitation!
              </h2>
              <h6 className="mt-3">
                Redirecting you to the home page in {this.state.seconds}
                {this.state.seconds > 1 ? "seconds" : "second"}
              </h6>
            </div>
          )}
          {this.state.success === false && (
            <div className="text-center">
              <h2 className="mt-2 text-bold">Oops! Something went wrong!</h2>
              <h6 className="mt-3">
                Please try again or contact us for assistance
              </h6>
            </div>
          )}
        </div>
      </div>
    );
  }
}
confirmEventInvite.propTypes = {
  location: PropTypes.object,
  history: PropTypes.object,
  match: PropTypes.object
};
export default confirmEventInvite;
