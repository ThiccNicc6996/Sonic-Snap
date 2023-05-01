using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PowerUtils {
    public class Modifier
    {
        public string sourceName;
        public string modType;
        public int modValue;

        public Modifier(string name, string type, int value) {
            sourceName = name;
            modType = type;
            modValue = value;
        }

        /******************
        * Accessor methods*
        ******************/

        public string getSource() {
            return sourceName;
        }

        public string getType() {
            return modType;
        }

        public int getValue() {
            return modValue;
        }
    }
}