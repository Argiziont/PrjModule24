import React, { useState, useEffect } from "react";
import { UserActions } from "../../_actions";

import MonetizationOnIcon from "@material-ui/icons/MonetizationOn";
import { makeStyles } from "@material-ui/core/styles";
import Avatar from "@material-ui/core/Avatar";
import TextField from "@material-ui/core/TextField";
import CssBaseline from "@material-ui/core/CssBaseline";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import InputAdornment from "@material-ui/core/InputAdornment";
import VerifiedUser from "@material-ui/icons/VerifiedUser";
import Typography from "@material-ui/core/Typography";
import Container from "@material-ui/core/Container";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Switch from "@material-ui/core/Switch";
import Link from "@material-ui/core/Link";
import CircularProgress from "@material-ui/core/CircularProgress";
import Snackbar from "@material-ui/core/Snackbar";
import MuiAlert, { AlertProps, Color } from "@material-ui/lab/Alert";
import NumberFormat from "react-number-format";

function Alert(props: AlertProps) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    margin: theme.spacing(2),
  },
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  avatar: {
    width: theme.spacing(15),
    height: theme.spacing(15),
    margin: theme.spacing(1),
    backgroundColor: "#BDBDBD",
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  userIcon: {
    width: theme.spacing(7),
    height: theme.spacing(7),
  },
  margin: {
    margin: theme.spacing(1),
  },
  inputLabel: {
    margin: theme.spacing(5),
  },
  inputButton: {
    width: "100%",
    height: "200%",
  },
  typography: {
    marginBottom: theme.spacing(5),
  },
}));

export interface SnackbarMessage {
  message: string;
  type: Color;
  key: number;
}

export interface State {
  open: boolean;
  snackPack: SnackbarMessage[];
  messageInfo?: SnackbarMessage;
}

interface NumberFormatCustomProps {
  inputRef: (instance: NumberFormat | null) => void;
  onChange: (event: { target: { name: string; value: string } }) => void;
  name: string;
}

function NumberFormatCustom(props: NumberFormatCustomProps) {
  const { inputRef, onChange, ...other } = props;

  return (
    <NumberFormat
      {...other}
      getInputRef={inputRef}
      onValueChange={(values) => {
        onChange({
          target: {
            name: props.name,
            value: values.value,
          },
        });
      }}
      isAllowed={(values) => {
        const { floatValue } = values;
        if (floatValue != undefined) {
          if (floatValue >= 0 && floatValue <= 99999) {
            return true;
          } else {
            return false;
          }
        } else {
          return false;
        }
      }}
      thousandSeparator
      isNumericString
    />
  );
}

export const HomePage: React.FC = () => {
  const classes = useStyles();
  const [userBalance, setUserBalance] = useState<string>();
  const [isLodaing, setIsLodaing] = useState<boolean>(true);
  const [amountForDeposit, setAmountForDeposit] = useState<number>(0);
  const [amountForWithdrawal, setAmountForWithdrawal] = useState<number>(0);
  const [userAccountState, setUserAccountState] = useState<boolean>(true);
  const [openSnack, setOpenSnack] = React.useState<boolean>(false);
  const [snackPack, setSnackPack] = React.useState<SnackbarMessage[]>([]);
  const [messageInfo, setMessageInfo] = React.useState<
    SnackbarMessage | undefined
  >(undefined);

  const handleSnackOpen = (message: string, type: Color) => () => {
    setSnackPack((prev) => [
      ...prev,
      { message, key: new Date().getTime(), type },
    ]);
  };

  const handleSnackClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === "clickaway") {
      return;
    }

    setOpenSnack(false);
  };

  const handleSnackExited = () => {
    setMessageInfo(undefined);
  };

  const handleDepositChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAmountForDeposit(Number(event.target.value));
  };

  const handleWithdrawalChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setAmountForWithdrawal(Number(event.target.value));
  };

  const onWithdrawalClick = async () => {
    try {
      await UserActions.withdrawalFromAccout(amountForWithdrawal || 0);
      handleSnackOpen("Money withdrawal success", "success")();
    } catch (error) {
      handleSnackOpen(error, "error")();
    }
    UserActions.getBalance().then((moneyAmount) => {
      setUserBalance(moneyAmount);
    });
    setAmountForWithdrawal(0);
  };

  const onDepositClick = async () => {
    try {
      await UserActions.depositToAccount(amountForDeposit || 0);
      handleSnackOpen("Money deposit success", "success")();
    } catch (error) {
      handleSnackOpen(error, "error")();
    }
    UserActions.getBalance().then((moneyAmount) => {
      setUserBalance(moneyAmount);
    });
    setAmountForDeposit(0);
  };

  const onAccountStateChange = async () => {
    if (userAccountState) {
      try {
        await UserActions.closeAccount();
        handleSnackOpen("Account closed", "info")();
      } catch (error) {
        handleSnackOpen(error, "error")();
      }
    }
    if (!userAccountState) {
      try {
        await UserActions.openAccount();
        handleSnackOpen("Account opened", "success")();
      } catch (error) {
        handleSnackOpen(error, "error")();
      }
    }

    setUserAccountState(!userAccountState);
  };

  const onLogoutClick = () => {
    UserActions.logout();
  };

  useEffect(() => {
    let isMounted = true;
    setIsLodaing(true);

    UserActions.getBalance().then((moneyAmount) => {
      if (isMounted) {
        setUserBalance(moneyAmount);
      }
      UserActions.getState().then((state) => {
        if (isMounted) {
          setUserAccountState(state == "True" ? true : false);
          setIsLodaing(false);
        }
      });
    });
    return () => {
      isMounted = false;
    }; // use effect cleanup to set flag false, if unmounted
  }, []);

  useEffect(() => {
    if (snackPack.length && !messageInfo) {
      // Set a new snack when we don't have an active one
      setMessageInfo({ ...snackPack[0] });
      setSnackPack((prev) => prev.slice(1));
      setOpenSnack(true);
    } else if (snackPack.length && messageInfo && open) {
      // Close an active snack when a new one is added
      setOpenSnack(false);
    }
  }, [snackPack, messageInfo, openSnack]);

  return isLodaing ? (
    <Grid
      container
      spacing={0}
      direction="column"
      alignItems="center"
      justify="center"
      style={{ minHeight: "100vh" }}
    >
      <Grid item xs={3}>
        <CircularProgress />
      </Grid>
    </Grid>
  ) : (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <VerifiedUser className={classes.userIcon} />
        </Avatar>

        <Typography component="h1" variant="h5" className={classes.typography}>
          Successfully logged in
        </Typography>

        <div className={classes.root}>
          <Grid container spacing={4} justify="center">
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                label="Your balance"
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <MonetizationOnIcon />
                      <Typography
                        component="h1"
                        variant="body1"
                        className={classes.margin}
                      >
                        {userBalance}
                      </Typography>
                    </InputAdornment>
                  ),
                  readOnly: true,
                }}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                disabled={!userAccountState}
                variant="outlined"
                label="Amount of deposit"
                value={amountForDeposit}
                onChange={handleDepositChange}
                InputProps={{
                  // eslint-disable-next-line @typescript-eslint/no-explicit-any
                  inputComponent: NumberFormatCustom as any,
                  startAdornment: (
                    <InputAdornment position="start">
                      <MonetizationOnIcon />
                    </InputAdornment>
                  ),
                }}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <Button
                disabled={!userAccountState}
                size="large"
                variant="contained"
                onClick={async () => {
                  await onDepositClick();
                }}
              >
                <Typography
                  component="h1"
                  variant="body1"
                  className={classes.margin}
                >
                  Submit
                </Typography>
              </Button>
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                disabled={!userAccountState}
                variant="outlined"
                label="Amount of withdrawal"
                value={amountForWithdrawal}
                onChange={handleWithdrawalChange}
                InputProps={{
                  // eslint-disable-next-line @typescript-eslint/no-explicit-any
                  inputComponent: NumberFormatCustom as any,
                  startAdornment: (
                    <InputAdornment position="start">
                      <MonetizationOnIcon />
                    </InputAdornment>
                  ),
                }}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <Button
                disabled={!userAccountState}
                size="large"
                variant="contained"
                onClick={async () => {
                  await onWithdrawalClick();
                }}
              >
                <Typography
                  component="h1"
                  variant="body1"
                  className={classes.margin}
                >
                  Submit
                </Typography>
              </Button>
            </Grid>
            <Grid item>
              <FormControlLabel
                control={
                  <Switch
                    size="medium"
                    name="AccountOpenState"
                    color="primary"
                    checked={userAccountState}
                    onChange={async () => {
                      await onAccountStateChange();
                    }}
                  />
                }
                label="Account state"
              />
            </Grid>
            <Grid item xs={12} sm={12}>
              <Link href="#" variant="body2" onClick={onLogoutClick}>
                {"Want to exit? Log out"}
              </Link>
            </Grid>
          </Grid>
        </div>
      </div>
      <Snackbar
        key={messageInfo ? messageInfo.key : undefined}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "left",
        }}
        open={openSnack}
        autoHideDuration={6000}
        onClose={handleSnackClose}
        onExited={handleSnackExited}
      >
        <Alert
          onClose={handleSnackClose}
          severity={messageInfo ? messageInfo.type : undefined}
        >
          {messageInfo ? messageInfo.message : undefined}
        </Alert>
      </Snackbar>
    </Container>
  );
};
