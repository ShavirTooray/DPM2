package student.projects.leaselinkapplication.ManagerLanding

import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import student.projects.leaselinkapplication.R

class UpdateListingsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activty_update_listing)

        val addButton = findViewById<Button>(R.id.button18)
        val updateButton = findViewById<Button>(R.id.button20)

        // Updated to match new XML (four cards: imageButton1..4)
        val propertyImage1 = findViewById<ImageButton>(R.id.imageButton1)
        val propertyImage2 = findViewById<ImageButton>(R.id.imageButton2)
        val propertyImage3 = findViewById<ImageButton>(R.id.imageButton3)
        val propertyImage4 = findViewById<ImageButton>(R.id.imageButton4)

        addButton.setOnClickListener {
            Toast.makeText(this, "Add New Listing clicked", Toast.LENGTH_SHORT).show()
        }

        updateButton.setOnClickListener {
            Toast.makeText(this, "Update Listing clicked", Toast.LENGTH_SHORT).show()
        }

        propertyImage1.setOnClickListener {
            Toast.makeText(this, "Property 1 selected", Toast.LENGTH_SHORT).show()
        }
        propertyImage2.setOnClickListener {
            Toast.makeText(this, "Property 2 selected", Toast.LENGTH_SHORT).show()
        }
        propertyImage3.setOnClickListener {
            Toast.makeText(this, "Property 3 selected", Toast.LENGTH_SHORT).show()
        }
        propertyImage4.setOnClickListener {
            Toast.makeText(this, "Property 4 selected", Toast.LENGTH_SHORT).show()
        }
    }
}

