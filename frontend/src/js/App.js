import React, { Component } from 'react';

import App from 'grommet/components/App';
import Title from 'grommet/components/Title';
import Section from 'grommet/components/Section';
import Article from 'grommet/components/Article';
import Form from 'grommet/components/Form';
import FormFields from 'grommet/components/FormFields';
import Button from 'grommet/components/Button';
import Heading from 'grommet/components/Heading';
import Header from 'grommet/components/Header';
import Footer from 'grommet/components/Footer';


export default class BasicApp extends Component {
  render() {
    return (
      <App>
        <Article scrollStep={false} controls={false}>
          <Section>
            <Title>Hello World</Title>
            <p>Hello from a Grommet page!</p>
          </Section>
          <Section>
            <Form>
              <Header>
                <Heading>
                  Sample Header
                </Heading>
              </Header>
              <FormFields>
                <t />
              </FormFields>
              <Footer pad={{ "vertical": "medium" }}>
                <Button label='Submit'
                  type='button'
                  primary={true}
                  onClick={console.log} />
              </Footer>
            </Form>
          </Section>
        </Article>
      </App>
    );
  }
}
