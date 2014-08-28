/*
nwitsml Copyright 2010 Setiri LLC
Derived from the jwitsml project, Copyright 2010 Statoil ASA
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
namespace witsmllib
{

    /**
     * Interface for clients that wants to be notified about details in
     * the remote WITSML server access.
     *
     * @see WitsmlServer#addAccessListener(WitsmlAccessListener)
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    public interface WitsmlAccessListener
    {

        /**
         * This method is called whenever the remote WITSML server has
         * been accessed.
         *
         * @param event  Event describing the access. Never null.
         */
      
        //TODO
       // public void accessPerformed(WitsmlAccessEvent @event);
    }
}