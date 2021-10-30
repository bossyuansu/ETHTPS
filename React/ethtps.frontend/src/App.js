import './App.css';
import githubIcon from './assets/600px-Octicons-mark-github.svg - inv.png';
import twitterIcon from './assets/1486053611-twitter_79195.png';
import discordIcon from './assets/discord-mascot.png';
import { globalApi } from './services/common';
import React, { useState, useEffect } from "react";
import InstantTPSStat from './components/InstantTPSStat';
import Timeline from './components/Timeline';
import HorizontalBarChart from './components/HorizontalBarChart'
import { render } from 'react-dom';

class App extends React.Component {

  constructor(props){
    super(props);

    this.state = {
      homePageModel: {
        instantTPS: {}
      },
      network: "Mainnet"
    }
  }
  
  //const [isDarkMode, setIsDarkMode] = useState(() => false);

  /*
  
  <DarkModeToggle
  className={'modeSwitchToggle'}
    onChange={setIsDarkMode}
    checked={isDarkMode}
    size={80}
  />
  */

componentDidMount(){
  globalApi.aPIV2HomePageModelGet(this.state.network, (err,data,res) => {
    console.log(data)
     this.setState({homePageModel: data});
  });
}

 render(){
  return (
    <>
    <center>
    <br></br>
    <div className={"jumpy unselectable"}>ETHTPS.info</div>
    <br></br>
    <br></br>
      <a href="https://github.com/WhoEvenAmI/ETHTPS">
          <img className={"small-img"} src={githubIcon}>
          </img>
        </a>
        <a href="https://twitter.com/ethtps">
          <img className={"small-img"} src={twitterIcon}>
          </img>
        </a>
        <a href="https://discord.gg/jWPcsTzpCT">
          <img className={"small-img"} src={discordIcon}>
          </img>
        </a>
    </center>
    <InstantTPSStat/>
    <Timeline/>
    <HorizontalBarChart data={this.state.homePageModel.instantTPS} colorDictionary={this.state.homePageModel.colorDictionary} providerData={this.state.homePageModel.providerData}/>
    <footer>
      <div className={'inline'}>
      Brought to you by 
      <div style={{marginLeft:'5px'}} className={'trick'}>
        <span>
          Mister_Eth
        </span>
      </div>
      <br></br>
        Donate: 
        <p className={'ul-hover inline'} style={{marginLeft:'5px'}}>
          0x466Ef24d68ac9b61670eeE7fC2E001B951aBf482
        </p>
      </div>
    </footer>
    </>
  );
}
}
export default App;
