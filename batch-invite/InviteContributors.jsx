import React from "react";
import swal from "sweetalert";
import { Card, CardHeader, Col, Row, CardBody } from "reactstrap";
import * as eventContributorsService from "../../../services/eventContributorsService";
import logger from "../../../logger";
import { Formik, Field, Form, ErrorMessage } from "formik";
import { volunteerInvitationSchema } from "../../../validationSchemas";
import Autocomplete from "../../autocomplete/AutoCompleteUsers";
import PropTypes from "prop-types";

const _logger = logger.extend("invitation");

class InviteVolunteers extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isSearching: false,
      searchQuery: "",
      volunteers: [],
      selectedUsers: [],
      userComponent: [],
      contributionTypes: [],
      eventId: 0,
      contributionTypeId: 0
    };
  }
  componentDidMount() {
    eventContributorsService
      .getAllTypes()
      .then(this.onGetAllTypesSuccess)
      .catch(this.onError);
  }

  onGetAllTypesSuccess = response => {
    this.setState({
      contributionTypes: response.items,
      eventId: this.props.eventId
    });
    _logger(this.state)
  };
  add = values => {
    let newVolunteers = [...this.state.volunteers];
    newVolunteers.push(values);
    this.setState({
      volunteers: newVolunteers
    });
    _logger(this.state.volunteers);
  };
  invite = values => {
    _logger(values);
    const contributionTypeId = Number(values.contributionTypeId);
    const contributionTypes = [...this.state.contributionTypes];
    const index = contributionTypes
      .map(item => item.id)
      .indexOf(contributionTypeId);
    _logger(this.state.contributionTypes[index]);
    const contributionType = this.state.contributionTypes[index];
    let selectedUsers = this.state.selectedUsers;
    for (let i = 0; i < this.state.selectedUsers.length; i++) {
      selectedUsers[i].contributionTypeId = contributionTypeId;
      selectedUsers[i].ContributionType = contributionType.name;
    }

    eventContributorsService
      .invite(this.state.selectedUsers)
      .then(this.onInviteSuccess)
      .catch(this.onError);
    _logger(selectedUsers);
    this.setState({ selectedUsers: [], userComponent: [] });
  };
  onInviteSuccess = () => {
    swal({
      title: "Invitation has been sent!",
      icon: "success",
      timer: 2000
    });
  };
  searchUser = userInput => {
    return eventContributorsService.search(userInput);
  };
  onSearchSuccess = () => {
    _logger("success");
  };
  setSuggestions = (selectedUsers, userComponent) => {
    let users = selectedUsers.map(user => {
      return {
        userId: user.userId,
        firstName: user.firstName,
        lastName: user.lastName,
        to: user.to,
        eventId: this.state.eventId,
        contributionTypeId: 0
      };
    });
    this.setState(() => {
      return {
        selectedUsers: users,
        userComponent
      };
    });
  };
  render() {
    const autoCompleteProps = {
      searchUser: this.searchUser,
      selectedUsers: this.state.selectedUsers,
      userComponent: this.state.userComponent,
      setSuggestions: this.setSuggestions,
      isBatchInsert: true
    };
    const contributionTypes = this.state.contributionTypes;
    return (
      <>
        <Card className="mt-4">
          <CardHeader>
            <h3 className="text-center">Invite Contributors</h3>
          </CardHeader>
          <CardBody>
            <Row>
              <Col>
                <Autocomplete {...autoCompleteProps} />
                <Formik
                  initialValues={this.state.contributionTypeId}
                  enableReinitialize={true}
                  validationSchema={volunteerInvitationSchema}
                  onSubmit={(values, actions) => {
                    this.invite(values);
                    actions.setSubmitting(false);
                  }}
                >
                  {({ errors, touched }) => (
                    <Form>
                      <label className="col-form-label">
                        Contribution Type
                      </label>
                      <Field
                        className={
                          "form-control" +
                          (!errors.contributionTypeId &&
                          touched.contributionTypeId
                            ? " is-valid"
                            : "") +
                          (errors.contributionTypeId &&
                          touched.contributionTypeId
                            ? " is-invalid"
                            : "")
                        }
                        component="select"
                        name="contributionTypeId"
                      >
                        <option value={-1}>Select Contribution Type</option>
                        {contributionTypes.map(item => {
                          return (
                            <option key={item.id} value={item.id}>
                              {item.name}
                            </option>
                          );
                        })}
                      </Field>
                      <ErrorMessage
                        name="contributionTypeId"
                        component="div"
                        className="invalid-feedback"
                      />
                      <button type="submit" className="btn btn-info mt-3 ml-2 float-right">
                        Invite
                      </button>
                    </Form>
                  )}
                </Formik>
              </Col>
            </Row>
          </CardBody>
        </Card>
      </>
    );
  }
}
InviteVolunteers.propTypes = {
  location: PropTypes.any,
  history: PropTypes.object,
  match: PropTypes.object,
  eventId: PropTypes.any, 
  autoCompleteProps: PropTypes.shape({
    searchUser: PropTypes.func,
    selectedUsers: PropTypes.array,
    userComponent: PropTypes.array,
    setSuggestions: PropTypes.func
  })
};
export default InviteVolunteers;
